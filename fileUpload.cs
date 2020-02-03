[HttpPost]
        [Route("fileUplaod")]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var filePath = string.Empty;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                      filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            try
            {
                System.IO.File.Move(filePath, "C:\\Users\\jibin.jh\\Downloads\\New folder\\Test1");
            }
            catch (Exception ex)
            {

                throw;
            }
            
           // MoveImage(filePath);
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }
