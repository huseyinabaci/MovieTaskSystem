using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSystem.Common.Constants;
using TaskSystem.Domain;
using TaskSystem.Infrastructure.Repository.Interfaces;

namespace TaskSystem.BackgrodundServices
{
    public sealed class TaskBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TaskBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var outboxRepo = scope.ServiceProvider.GetRequiredService<IRepository<UserCityDistrictOutbox>>();
                var cityRepo = scope.ServiceProvider.GetRequiredService<IRepository<UserCityDistrict>>();
                var fluentEmail = scope.ServiceProvider.GetRequiredService<IFluentEmail>();

                var outboxes = await outboxRepo.GetListAsync(
                    predicate: p => !p.IsCompleted,
                    orderBy: p => p.OrderBy(i => i.CreateAt),
                    disableTracking: false);

                foreach (var item in outboxes)
                {
                    try
                    {
                        if (item.Attempt >= 3)
                        {
                            item.IsCompleted = true;
                            item.CompleteDate = DateTimeOffset.Now;
                            item.IsFailed = true;
                            item.FailMessage = "Mail gönderme başarısız";
                            await outboxRepo.UpdateAsync(item);
                            continue;
                        }

                        var userCityDistrictInfo = await cityRepo.GetFirstOrDefaultAsync(
                            predicate: i => !i.IsDeleted && i.Id == item.UserCityDistrictId,
                            include:i => i.Include(i => i.City).ThenInclude(i => i.Districts),
                            disableTracking: false);

                        string body = TaskConstants.TaskMailTemplate;

                        body = body.Replace(TaskConstants.CityName, userCityDistrictInfo.City.Name)
                                   .Replace(TaskConstants.DistrictName, userCityDistrictInfo.District.Name)
                                   .Replace("Population", userCityDistrictInfo.Population.ToString())
                                   .Replace(TaskConstants.InsertedDate, userCityDistrictInfo.InsertedDate?.ToString("dd/MM/yyyy HH:mm:ss"));

                        var response = await fluentEmail
                            .To(TaskConstants.AdminUser)
                            .Subject("Oluşturulan Veri")
                            .Body(body)
                            .SendAsync(stoppingToken);

                        if (!response.Successful)
                        {
                            item.Attempt++;
                        }
                        else
                        {
                            item.IsCompleted = true;
                            item.CompleteDate = DateTimeOffset.Now;
                        }
                    }
                    catch (Exception ex)
                    {
                        item.Attempt++;
                        if (item.Attempt >= 3)
                        {
                            item.IsFailed = true;
                            item.IsCompleted = true;
                            item.CompleteDate = DateTimeOffset.Now;
                            item.FailMessage = ex.Message;
                        }
                    }

                    await outboxRepo.UpdateAsync(item);
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
