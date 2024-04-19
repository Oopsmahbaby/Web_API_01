using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_API.Data;
using Web_API.Models;

namespace Web_API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly Book_Store _context;
        private readonly IMapper _mapper;

        public BookRepository(Book_Store context,IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> addBookAsync(BookModel model)
        {
            var newBook=_mapper.Map<Book>(model);
            _context.Books!.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }

        public async Task deleteBookAsync(int id)
        {
            var delBook=_context.Books!.SingleOrDefault(x => x.Id == id);
            if (delBook != null)
            {
                _context.Books!.Remove(delBook);
                await _context.SaveChangesAsync() ;
            }
        }

        public async Task<List<BookModel>> getAllBooksAsync()
        {
            var books =await _context.Books!.ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> getBookAsync(int id)
        {
            var book = await _context.Books!.FindAsync(id);
            return _mapper.Map<BookModel>(book);
        }

        public async Task updateBookAsync(int id, BookModel model)
        {
            if(id== model.Id)
            {
                var updBook=_mapper.Map<Book>(model);
                _context.Books!.Update(updBook);
                await _context.SaveChangesAsync();
            }
        }
    }
}
