using FlashGameCofe.Dto.NoteDto;

namespace FlashGameCofe.Services.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NotesDto>> GetAllNotes();

        Task<NotesDto> GetNotesById(int Id);

        Task AddNote(AddNotesDto note);

        Task UpdateNote(UpdateNotesDto notesDto);

        Task DeleteNote(int Id);
    }
}
