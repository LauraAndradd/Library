﻿using Library.Application.Interfaces;
using Library.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookInputModel request)
        {
            var result = await _bookService.AddBookAsync(request);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.GetAllBooksAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _bookService.GetBookByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBook(int id)
        {
            var result = await _bookService.RemoveBookAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
    }
}
