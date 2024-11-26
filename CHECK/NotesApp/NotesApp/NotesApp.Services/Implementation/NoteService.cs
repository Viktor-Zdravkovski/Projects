﻿using NotesApp.DataAccess;
using NotesApp.DataAccess.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Dto.NoteDto;
using NotesApp.Mappers;
using NotesApp.Services.Interfaces;
using NotesApp.Shared.CustomException;

namespace NotesApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IUserRepository _userRepository;

        public NoteService(IRepository<Note> noteRepository, IUserRepository userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public List<NoteDto> GetAllNotes(int userId)
        {
            var notesDb = _noteRepository.GetAll().Where(n => n.UserId == userId);

            if (!notesDb.Any())
            {
                return new List<NoteDto>();
            }

            return notesDb.Select(x => x.ToNoteDto()).ToList();
        }

        public NoteDto GetNoteById(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found!");
            }

            NoteDto noteDto = noteDb.ToNoteDto();
            return noteDto;
        }

        public void AddNote(AddNoteDto addNoteDto)
        {
            User userDb = _userRepository.GetById(addNoteDto.UserId);
            if (userDb == null)
            {
                throw new NoteDataException($"User with id {addNoteDto.UserId} does not exist!");
            }

            if (string.IsNullOrEmpty(addNoteDto.Text))
            {
                throw new NoteDataException("Text is required field");
            }

            if (addNoteDto.Text.Length > 250)
            {
                throw new NoteDataException("Text can not contain more than 250 characters!");
            }

            Note newNote = addNoteDto.ToNote();
            newNote.User = userDb;

            _noteRepository.Add(newNote);
        }

        public void UpdateNote(UpdateNoteDto note)
        {
            Note noteDb = _noteRepository.GetById(note.Id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {note.Id} was not found!");
            }
            User userDb = _userRepository.GetById(note.UserId);
            if (userDb == null)
            {
                throw new NoteNotFoundException($"User with id {note.UserId} was not found!");
            }

            if (string.IsNullOrEmpty(note.Text))
            {
                throw new NoteDataException("Text is required field");
            }

            if (note.Text.Length > 250)
            {
                throw new NoteDataException("Text can not contain more than 250 characters!");
            }

            noteDb.Text = note.Text;
            noteDb.Tag = note.Tag;
            noteDb.Priority = note.Priority;
            noteDb.UserId = note.UserId;
            noteDb.User = userDb;

            _noteRepository.Update(noteDb);
        }

        public void DeleteNote(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found!");
            }

            _noteRepository.Delete(noteDb);
        }

    }
}
