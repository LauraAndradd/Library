﻿using Library.Application.Models;
using MediatR;

namespace Library.Application.Commands.UpdateBookCommand
{
    public class UpdateBookCommand : IRequest<ResultViewModel>
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
    }
}
