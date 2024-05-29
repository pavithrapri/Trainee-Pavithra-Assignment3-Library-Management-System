using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        private readonly ICosmosDbService<Issue> _cosmosDbService;

        public IssueController(ICosmosDbService<Issue> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<Issue>> GetAllIssues()
        {
            return await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Issue>> GetIssueById(string id)
        {
            var issue = await _cosmosDbService.GetItemAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            return issue;
        }

        [HttpPost]


        public async Task<ActionResult> AddIssue(Issue issue)
        {
            await _cosmosDbService.AddItemAsync(issue, issue.UId);
            return CreatedAtAction(nameof(GetIssueById), new { id = issue.UId }, issue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(string id, Issue issue)
        {
            if (id != issue.UId)
            {
                return BadRequest();
            }

            await _cosmosDbService.UpdateItemAsync(id, issue);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
