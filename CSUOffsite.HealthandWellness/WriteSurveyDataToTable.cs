using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Data.Tables;

namespace CSUOffsite.HealthandWellness
{
    public static class WriteSurveyDataToTable
    {
        private const string _storageAccounConnectionString = "AzureStorage_ConString";
        private static readonly string StorageAccounConnectionString = Environment.GetEnvironmentVariable(_storageAccounConnectionString);
        private const string _storageAccountTableName = "AzureStorage_TableName";
        private static readonly string StorageAccountTableName = Environment.GetEnvironmentVariable(_storageAccountTableName);
        private const double _sleepingHabitWeightage = 0.15,
            _appetiteWeightage = 0.25,
            _emotionalHealthWeightage = 0.30,
            _meTimeWeightage = 0.15,
            _workSatisfaction = 0.15;

        public enum SMARTMarksAssignment
        {
           Low=1,
           Average=2,
           Good=3,
           Better=4,
           Excellent=5       
        }

        [FunctionName("WriteSurveyDataToTable")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("Beginning to parse the request body");
            try
            {
                int _prevWeekSleepQuality =0, _morningEnergyLevel=0, _regularSleepPattern=0, _prevWeekSleeplessNight=0,
                    _appetiteStrength=0, _workdayAppetite=0, _nonWorkDayAppetite=0, _foodNutritionalVal=0,
                    _feelingAnxious=0, _troubleRelaxing=0, _anxietyStatus=0, _anxietyLevel=0,
                    _disconnect=0, _hasting=0, _feelingConnected=0, _timeManagement=0,
                    _happyIndex=0, _feelingValued=0, _workLifeBalance=0, _jobSatisfaction=0;

                string prevWeekSleepQuality =string.Empty, morningEnergyLevel = string.Empty, regularSleepPattern = string.Empty, prevWeekSleeplessNight = string.Empty,
                    appetiteStrength = string.Empty, workdayAppetite = string.Empty, nonWorkDayAppetite = string.Empty, foodNutritionalVal = string.Empty,
                    feelingAnxious = string.Empty, troubleRelaxing = string.Empty, anxietyStatus = string.Empty, anxietyLevel = string.Empty,
                    disconnect = string.Empty, hasting = string.Empty, feelingConnected = string.Empty, timeManagement = string.Empty,
                    happyIndex = string.Empty, feelingValued = string.Empty, workLifeBalance = string.Empty, jobSatisfaction = string.Empty;

                int _sleepingHabitTotalScore = 0,
                    _appetiteTotalScore = 0,
                    _emotionalHealthTotalScore = 0,
                    _workSatisfactionTotalScore = 0,
                    _meTimeTotalScore = 0,
                    _totalScore=0;
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);                              
                log.LogInformation($"Reading key value from deserialized data");
                switch (data?.prevWeekSleepQuality.ToString().ToLower())
                {
                    case "low":
                        _prevWeekSleepQuality = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _prevWeekSleepQuality = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _prevWeekSleepQuality = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _prevWeekSleepQuality = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _prevWeekSleepQuality = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.morningEnergyLevel.ToString().ToLower())
                {
                    case "low":
                        _morningEnergyLevel = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _morningEnergyLevel = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _morningEnergyLevel = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _morningEnergyLevel = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _morningEnergyLevel = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.regularSleepPattern.ToString().ToLower())
                {
                    case "low":
                        _regularSleepPattern = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _regularSleepPattern = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _regularSleepPattern = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _regularSleepPattern = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _regularSleepPattern = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.prevWeekSleeplessNight.ToString().ToLower())
                {
                    case "low":
                        _prevWeekSleeplessNight = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _prevWeekSleeplessNight = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _prevWeekSleeplessNight = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _prevWeekSleeplessNight = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _prevWeekSleeplessNight = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.appetiteStrength.ToString().ToLower())
                {
                    case "low":
                        _appetiteStrength = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _appetiteStrength = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _appetiteStrength = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _appetiteStrength = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _appetiteStrength = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.workdayAppetite.ToString().ToLower())
                {
                    case "low":
                        _workdayAppetite = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _workdayAppetite = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _workdayAppetite = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _workdayAppetite = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _workdayAppetite = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.nonWorkDayAppetite.ToString().ToLower())
                {
                    case "low":
                        _nonWorkDayAppetite = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _nonWorkDayAppetite = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _nonWorkDayAppetite = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _nonWorkDayAppetite = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _nonWorkDayAppetite = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.foodNutritionalVal.ToString().ToLower())
                {
                    case "low":
                        _foodNutritionalVal = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _foodNutritionalVal = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _foodNutritionalVal = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _foodNutritionalVal = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _foodNutritionalVal = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.feelingAnxious.ToString().ToLower())
                {
                    case "low":
                        _feelingAnxious = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _feelingAnxious = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _feelingAnxious = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _feelingAnxious = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _feelingAnxious = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.troubleRelaxing.ToString().ToLower())
                {
                    case "low":
                        _troubleRelaxing = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _troubleRelaxing = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _troubleRelaxing = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _troubleRelaxing = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _troubleRelaxing = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.anxietyStatus.ToString().ToLower())
                {
                    case "low":
                        _anxietyStatus = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _anxietyStatus = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _anxietyStatus = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _anxietyStatus = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _anxietyStatus = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.anxietyLevel.ToString().ToLower())
                {
                    case "low":
                        _anxietyLevel = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _anxietyLevel = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _anxietyLevel = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _anxietyLevel = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _anxietyLevel = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.disconnect.ToString().ToLower())
                {
                    case "low":
                        _disconnect = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _disconnect = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _disconnect = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _disconnect = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _disconnect = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.hasting.ToString().ToLower())
                {
                    case "low":
                        _hasting = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _hasting = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _hasting = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _hasting = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _hasting = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.feelingConnected.ToString().ToLower())
                {
                    case "low":
                        _feelingConnected = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _feelingConnected = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _feelingConnected = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _feelingConnected = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _feelingConnected = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.timeManagement.ToString().ToLower())
                {
                    case "low":
                        _timeManagement = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _timeManagement = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _timeManagement = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _timeManagement = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _timeManagement = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.happyIndex.ToString().ToLower())
                {
                    case "low":
                        _happyIndex = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _happyIndex = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _happyIndex = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _happyIndex = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _happyIndex = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.feelingValued.ToString().ToLower())
                {
                    case "low":
                        _feelingValued = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _feelingValued = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _feelingValued = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _feelingValued = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _feelingValued = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.workLifeBalance.ToString().ToLower())
                {
                    case "low":
                        _workLifeBalance = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _workLifeBalance = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _workLifeBalance = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _workLifeBalance = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _workLifeBalance = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                switch (data?.jobSatisfaction.ToString().ToLower())
                {
                    case "low":
                        _jobSatisfaction = (int)SMARTMarksAssignment.Low;
                        break;
                    case "average":
                        _jobSatisfaction = (int)SMARTMarksAssignment.Average;
                        break;
                    case "good":
                        _jobSatisfaction = (int)SMARTMarksAssignment.Good;
                        break;
                    case "better":
                        _jobSatisfaction = (int)SMARTMarksAssignment.Better;
                        break;
                    case "excellent":
                        _jobSatisfaction = (int)SMARTMarksAssignment.Excellent;
                        break;
                }
                _sleepingHabitTotalScore = _prevWeekSleepQuality + _morningEnergyLevel + _regularSleepPattern + _prevWeekSleeplessNight;
                _appetiteTotalScore = _appetiteStrength + _workdayAppetite + _nonWorkDayAppetite + _foodNutritionalVal;
                _emotionalHealthTotalScore = _feelingAnxious + _troubleRelaxing + _anxietyStatus + _anxietyLevel;
                _workSatisfactionTotalScore = _happyIndex + _feelingValued + _jobSatisfaction + _workLifeBalance;
                _meTimeTotalScore = _disconnect + _hasting + _feelingConnected + _timeManagement;
                _totalScore = _sleepingHabitTotalScore + _appetiteTotalScore + _emotionalHealthTotalScore + _workSatisfactionTotalScore + _meTimeTotalScore;

                SurveyEntityModel survey = new SurveyEntityModel
                {
                    PartitionKey = Guid.NewGuid().ToString(),
                    //Row key will represent survey id
                    RowKey = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTime.UtcNow,
                    UserPrincipalName = data?.upn,
                    Email=data?.Email,
                    SurveyTime=data?.SurveyTime,

                    PrevWeekSleepQuality = _prevWeekSleepQuality,
                    MorningEnergyLevel = _morningEnergyLevel,
                    RegularSleepPattern = _regularSleepPattern,
                    PrevWeekSleeplessNight = _prevWeekSleeplessNight,

                    AppetiteStrength = _appetiteStrength,     
                    WorkdayAppetite = _workdayAppetite,
                    NonWorkDayAppetite = _nonWorkDayAppetite,
                    FoodNutritionalVal = _foodNutritionalVal,

                    FeelingAnxious = _feelingAnxious,
                    TroubleRelaxing = _troubleRelaxing,
                    AnxietyStatus = _anxietyStatus,
                    AnxietyLevel = _anxietyLevel,

                    Disconnect = _disconnect,
                    Hasting = _hasting,
                    FeelingConnected = _feelingConnected,
                    TimeManagement = _timeManagement,

                    HappyIndex = _happyIndex,
                    FeelingValued = _feelingValued,
                    WorkLifeBalance = _workLifeBalance,
                    JobSatisfaction = _jobSatisfaction,

                    SleepingHabitTotalScore=_sleepingHabitTotalScore,
                    AppetiteTotalScore=_appetiteTotalScore,
                    EmotionalHealthTotalScore=_emotionalHealthTotalScore,
                    WorkProductivityTotalScore=_workSatisfactionTotalScore,
                    MeTimeTotalScore=_meTimeTotalScore,
                    TotalScore=_totalScore
                };
                CreateAzureStorageTableClient(survey, log);
                return new OkObjectResult($"Survey details added to Azure Table storage");
            }
            catch(Exception ex)
            {
                log.LogInformation($"Error occurred while adding survey to Azure Table Storage.\n{ex.Message}\n{ex.StackTrace}");
                return new BadRequestObjectResult($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private static async void CreateAzureStorageTableClient(SurveyEntityModel survey,ILogger log)
        {
            try
            {
                TableClient tableClient = new TableClient(StorageAccounConnectionString, StorageAccountTableName);
                await tableClient.CreateIfNotExistsAsync();
                tableClient.AddEntity(survey);
            }
            catch (Exception ex)
            {
                log.LogInformation($"Error occurred while adding entity to the table: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
