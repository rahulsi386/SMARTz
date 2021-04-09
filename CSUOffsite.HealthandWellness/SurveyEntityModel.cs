using System;
using System.Collections.Generic;
using System.Text;
using Azure;
using Azure.Data.Tables;

namespace CSUOffsite.HealthandWellness
{
    internal class SurveyEntityModel : ITableEntity
    {
        public string PartitionKey { get; set; }
        //RowKey indicates Survey ID
        public string RowKey { get; set; }

        //Timestamp and Etag properties are automatically populated by the system and need not be supplied by the user.
        public DateTimeOffset? Timestamp { get; set; }
        //ETag type is part of Azure.Core assembly and Azure namespace
        public ETag ETag { get; set; }
        public string Email { get; set; }
        public string SurveyTime { get; set; }

        //SleepingHabit 
        public int PrevWeekSleepQuality { get; set; }
        public int MorningEnergyLevel { get; set; }
        public int RegularSleepPattern { get; set; }
        public int PrevWeekSleeplessNight { get; set; }

        //Appetite
        public int AppetiteStrength { get; set; }
        public int WorkdayAppetite { get; set; }
        public int NonWorkDayAppetite { get; set; }
        public int FoodNutritionalVal { get; set; }

        //EmotionalHealth
        public int FeelingAnxious { get; set; }
        public int TroubleRelaxing { get; set; }
        public int AnxietyStatus { get; set; }
        public int AnxietyLevel { get; set; }

        //MeTime
        public int Disconnect{ get; set; }
        public int Hasting { get; set; }
        public int FeelingConnected { get; set; }
        public int TimeManagement { get; set; }

        //WorkSatisfaction
        public int HappyIndex { get; set; }
        public int FeelingValued { get; set; }
        public int WorkLifeBalance { get; set; }
        public int JobSatisfaction { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public string UserPrincipalName { get; set; }
        //Category-wise Score
        public int SleepingHabitTotalScore { get; set; }
        public int AppetiteTotalScore { get; set; }
        public int EmotionalHealthTotalScore { get; set; }
        public int WorkProductivityTotalScore { get; set; }
        public int MeTimeTotalScore { get; set; }

        public int TotalScore { get; set; }
       
    }
}
