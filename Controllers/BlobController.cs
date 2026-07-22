using Microsoft.AspNetCore.Mvc;
using VenuBooking.Services;

namespace VenuBooking.Controllers
{
    public class BlobController : Controller
    {
        private readonly BlobService _blobService;

        public BlobController(BlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task<IActionResult> Image(string fileName)
        {
            var file = await _blobService.GetFileAsync(fileName);

            if (file.stream == null || file.contentType == null)
            {
                return NotFound();
            }

            return File(file.stream, file.contentType);
        }
    }
}