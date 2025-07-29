using AutoMapper;
using FlashGameCofe.DataBase.Interfaces;
using FlashGameCofe.Domain.Models;
using FlashGameCofe.Dto.NoteDto;
using FlashGameCofe.Services.Interfaces;
using FlashGameCofe.Shared.CustomExceptions;

namespace FlashGameCofe.Services.Implementations
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IMapper _mapper;

        public NoteService(IRepository<Note> noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotesDto>> GetAllNotes()
        {
            var notes = await _noteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NotesDto>>(notes);
        }

        public async Task<NotesDto> GetNotesById(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            return _mapper.Map<NotesDto>(note);
        }

        public async Task AddNote(AddNotesDto noteDto)
        {
            if (noteDto == null)
            {
                throw new NoDataException("Note data is required!");
            }

            var noteEntity = _mapper.Map<Note>(noteDto);

            await _noteRepository.AddAsync(noteEntity);
        }

        public async Task UpdateNote(UpdateNotesDto updateNotesDto)
        {
            var entity = await _noteRepository.GetByIdAsync(updateNotesDto.Id);
            if (entity == null)
            {
                throw new Exception("Note not found.");
            }
            _mapper.Map(updateNotesDto, entity);
            await _noteRepository.UpdateAsync(entity);
        }

        public async Task DeleteNote(int id)
        {
            var entity = await _noteRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Note not found.");
            }
            await _noteRepository.DeleteAsync(entity);
        }
    }
}
