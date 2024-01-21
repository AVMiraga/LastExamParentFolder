using ExamTask.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask.Business.Services
{
    public class SettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, string> GetSettings()
        {
            Dictionary<string, string> settings = _context.Settings.ToDictionary(x => x.Key, x => x.Value);

            return settings;
        }
    }
}
