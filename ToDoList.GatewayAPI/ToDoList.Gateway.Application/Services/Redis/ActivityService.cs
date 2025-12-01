using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.interfaces;

namespace ToDoList.Gateway.Application.Services.Redis

{
    public class ActivityService
    {
        private readonly IRedisClient _redisClient;

        public ActivityService(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public async Task MarkUserActiveAsync(string userId)
        {
            await _redisClient.SetStringAsync($"gateway:active:{userId}", "1", TimeSpan.FromMinutes(5));
        }
    }
}

