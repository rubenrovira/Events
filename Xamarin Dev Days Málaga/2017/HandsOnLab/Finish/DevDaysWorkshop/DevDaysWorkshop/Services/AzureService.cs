﻿using DevDaysWorkshop.Models;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevDaysWorkshop.Services
{
    public class AzureService
    {
        private MobileServiceClient _client;
        private IMobileServiceTable<Speaker> _speakerTable;
        private static AzureService _instance;

        public static AzureService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AzureService();
                }

                return _instance;
            }
        }

        public AzureService()
        {
            if (_client != null)
                return;

            _client = new MobileServiceClient(AppSettings.AzureUrl);
            _speakerTable = _client.GetTable<Speaker>();
        }

        public Task<IEnumerable<Speaker>> ReadSpeakersAsync()
        {
            return _speakerTable.ReadAsync();
        }

        public async Task AddOrUpdateSpeakerAsync(Speaker speaker)
        {
            if (string.IsNullOrEmpty(speaker.Id))
            {
                await _speakerTable.InsertAsync(speaker);
            }
            else
            {
                await _speakerTable.UpdateAsync(speaker);
            }
        }

        public async Task DeleteSpeakerAsync(Speaker speaker)
        {
            await _speakerTable.DeleteAsync(speaker);
        }
    }
}