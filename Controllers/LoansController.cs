using BookLendingSystem.Data;
using BookLendingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookLendingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoansController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLoans()
        {
            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .ToList();
            return Ok(loans);
        }

        [HttpPost]
        public IActionResult CreateLoan(Loan loan)
        {
            var book = _context.Books.Find(loan.BookId);
            if (book == null || book.AvailableCopies <= 0)
                return BadRequest("Book is not available.");

            // Use LendDate from frontend if provided, otherwise use DateTime.Now
            loan.LendDate = loan.LendDate == default ? DateTime.Now : loan.LendDate;
            // Use ReturnDate from frontend if provided, otherwise leave as null
            _context.Loans.Add(loan);
            book.AvailableCopies--;
            book.IsAvailable = book.AvailableCopies > 0;
            _context.SaveChanges();

            return Ok(loan);
        }

        [HttpPost("{id}/return")]
        public IActionResult ReturnLoan(int id)
        {
            var loan = _context.Loans.Include(l => l.Book).FirstOrDefault(l => l.Id == id);
            if (loan == null) return NotFound();

            loan.ReturnDate = DateTime.Now;
            loan.Book.AvailableCopies++;
            loan.Book.IsAvailable = true;

            _context.SaveChanges();
            return Ok(loan);
        }
    }
}
