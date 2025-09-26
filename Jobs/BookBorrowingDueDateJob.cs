using e_library.Repositories;
using e_library.Services;

namespace e_library.Jobs
{
    public class BookBorrowingDueDateJob
    {
        private readonly IBookRepository _bookRepository;
        private readonly NotificationService _notificationService;
        public BookBorrowingDueDateJob(IBookRepository bookRepository, NotificationService notificationService)
        {
            _bookRepository = bookRepository;
            _notificationService = notificationService;
        }

        async public Task TrackDueDates()
        {
            Console.WriteLine("Tracking due dates ...");

            var CurrentBorrowings = await _bookRepository.GetCurrentlyBorrowedBooks();

            foreach (var Borrowing in CurrentBorrowings)
            {

                DateOnly due_date = DateOnly.FromDateTime(Borrowing.due_date);

                if (due_date <= DateOnly.FromDateTime(DateTime.UtcNow))
                {
                    // overdue
                    string message = $"The book '{Borrowing.title}' is overdue. Please return it as soon as possible.";
                    await _notificationService.CreateNotification(message, "alert", Borrowing.user_id);
                }
                else if (DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2)) >= due_date &&
                         DateOnly.FromDateTime(DateTime.UtcNow) < due_date)
                {
                    // due date within 2 days
                    var daysLeft = (due_date.ToDateTime(TimeOnly.MinValue) - DateTime.UtcNow.Date).Days;
                    string message = $"The book '{Borrowing.title}' is due in {daysLeft} day(s). Kindly ensure it is returned on time.";
                    await _notificationService.CreateNotification(message, "reminder", Borrowing.user_id);
                }

            }

        }

    }
}

