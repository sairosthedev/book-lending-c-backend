using Microsoft.AspNetCore.Mvc;
using BookLendingSystem.Data;
using BookLendingSystem.Models;

namespace BookLendingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MembersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMembers()
        {
            return Ok(_context.Members.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetMember(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpPost]
        public IActionResult CreateMember(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, Member updatedMember)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            member.FullName = updatedMember.FullName;
            member.Email = updatedMember.Email;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            _context.Members.Remove(member);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
