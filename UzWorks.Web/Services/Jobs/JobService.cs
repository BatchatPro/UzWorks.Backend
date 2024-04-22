using Newtonsoft.Json;
using UzWorks.Core.DataTransferObjects.Jobs;

namespace UzWorks.Web.Services.Jobs;

public class JobService : IJobService
{
    private readonly HttpClient _httpClient;
    public JobService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IList<JobVM>> GetAllJobs()
    {
        var response = await _httpClient.GetAsync("/api/Job/GetAll");
        IList<JobVM> result = new List<JobVM>();

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<IList<JobVM>>(content);
            return result;
        }
        return result;
    }

    public async Task<JobVM> GetJobById(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/Job/GetById/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JobVM>(content);
        }
        return null;
    }
}
