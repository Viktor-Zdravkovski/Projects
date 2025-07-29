using System.Security.Claims;
using FlashGameCofe.Dto.NoteDto;
using FlashGameCofe.Services.Interfaces;
using FlashGameCofe.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FlashGameCofe.WebApp.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("GetAllNotes")]
        public async Task<ActionResult<List<NotesDto>>> GetAllNotes()
        {
            try
            {
                string userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                var userId = User.FindFirstValue("userId");

                var notes = await _noteService.GetAllNotes();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpGet("GetNoteById/{id}")]
        public async Task<ActionResult<NotesDto>> GetNoteById(int id)
        {
            try
            {
                var note = await _noteService.GetNotesById(id);
                if (note == null)
                {
                    return NotFound("Note not found");
                }
                return Ok(note);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, Please contact admin");
            }
        }

        [HttpPost("AddNote")]
        public async Task<IActionResult> AddNote([FromBody] AddNotesDto notesDto)
        {
            try
            {
                if (notesDto == null)
                {
                    return BadRequest("Note data is required");
                }

                await _noteService.AddNote(notesDto);
                return Ok("Note added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occurred, Please contact admin");
            }
        }

        [HttpPut("UpdateNote")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateNotesDto updateNotesDto)
        {
            try
            {
                await _noteService.UpdateNote(updateNotesDto);
                return Ok("Note updated successfully");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occurred, Please contact admin");
            }
        }

        [HttpDelete("DeleteNoteById/{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            try
            {
                await _noteService.DeleteNote(id);
                return Ok($"The note with ID: {id} was successfully deleted");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured contact admin");
            }
        }
    }
}
