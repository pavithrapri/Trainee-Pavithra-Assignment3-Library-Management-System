using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ICosmosDbService<Member> _cosmosDbService;

        public MemberController(ICosmosDbService<Member> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            return await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMemberById(string id)
        {
            var member = await _cosmosDbService.GetItemAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return member;
        }

        [HttpPost]
        public async Task<ActionResult> AddMember(Member member)
        {
            await _cosmosDbService.AddItemAsync(member, member.UId);
            return CreatedAtAction(nameof(GetMemberById), new { id = member.UId }, member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(string id, Member member)
        {
            if (id != member.UId)
            {
                return BadRequest();
            }

            await _cosmosDbService.UpdateItemAsync(id, member);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
