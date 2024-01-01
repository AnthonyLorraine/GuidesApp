using System;
using GuidesApp.Services.GuidesAPI.Data;
using GuidesApp.Services.GuidesAPI.Models;
using GuidesApp.Services.GuidesAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GuidesApp.Services.GuidesAPI.Services
{
	public class GuidesService
	{
        private readonly AppDbContext _context;
        private readonly ILogger<GuidesService> _logger;

        public GuidesService(AppDbContext context, ILogger<GuidesService> logger)
        {
            _context = context;
            _logger = logger;
        }
            
        public async Task<IEnumerable<Guide>> GetGuidesAsync()
        {
            return await _context.Guides.ToListAsync();
        }

        public async Task<Guide?> GetGuideAsync(int id)
        {
            return await _context.Guides.FindAsync(id);
        }

        public async Task<Guide> PostGuideAsync(CreateGuideDto newGuide)
        {
            Guide guide = new()
            {
                Title = newGuide.Title,
                Subtitle = newGuide.Subtitle,
                Content = newGuide.Content,
                CreatedBy = newGuide.CreatedBy,
                CreatedByDisplayName = newGuide.CreatedByDisplayName
            };
            await _context.AddAsync(guide);
            await _context.SaveChangesAsync();
            return guide;
        }

        public async Task<Guide?> PutGuideAsync(Guide currentGuide, UpdateGuideDto updatedGuide)
        {
            currentGuide.Title = updatedGuide.Title;
            currentGuide.Subtitle = updatedGuide.Subtitle;
            currentGuide.Content = updatedGuide.Content;
            currentGuide.LastModifiedBy = updatedGuide.LastModifiedBy;
            currentGuide.LastModifiedByDisplayName = updatedGuide.LastModifiedByDisplayName;

            int recordsUpdatedCount = await _context.SaveChangesAsync();
            if (recordsUpdatedCount < 1)
            {
                return null;
            }

            return currentGuide;
        }

        public async Task<int> DeleteGuideAsync(Guide guide)
        {

            _context.Remove(guide);
            

            return await _context.SaveChangesAsync();
        }
    }
}

