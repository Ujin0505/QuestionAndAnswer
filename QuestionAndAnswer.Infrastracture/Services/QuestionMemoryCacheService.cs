using System.Runtime.CompilerServices;
using Microsoft.Extensions.Caching.Memory;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;

namespace QuestionAndAnswer.Infrastracture.Services
{
    public class QuestionMemoryCacheService: IQuestionMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;

        public QuestionMemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public QuestionResponce Get(int questionId)
        {
            QuestionResponce questionResponce = null;
            _memoryCache.TryGetValue(GetCacheKey(questionId), out questionResponce);
            return questionResponce;
        }
        
        public void Set(QuestionResponce questionResponce)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSize(1);     
            _memoryCache.Set(GetCacheKey(questionResponce.Id), questionResponce, cacheEntryOptions); 
        }

        public void Remove(int questionId)
        {
            _memoryCache.Remove(GetCacheKey(questionId));
        }
        
        private string GetCacheKey(int id) => $"Question-{id}"; 
    }
}