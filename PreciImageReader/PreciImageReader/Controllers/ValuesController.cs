using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PreciImageReader.CustomExceptionHandler;
using PreciImageReader.NlogTextFile;

namespace PreciImageReader.Controllers
{
    [Route("Precipoint")]  //Changes the route Url to be like http://localhost:51553/Precipoint/
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private INlogger nlogger;
        public ValuesController(INlogger logger)
        {
            this.nlogger = logger;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        

        /// <summary>
        /// This GET method will be called when given as tilesource url in openseadragon will be given http://localhost:51553/Precipoint/ 
        ///The request url will be generated according deepZom schema and will send the respective folder and image ID with URL
        ///placed in the folder dzc_output_files
        /// </summary>
        /// <param name="folderID"></param>
        /// <param name="ImageId"></param>
        /// <returns></returns>

        // GET Precipoint/folderId/Image_Id
        [HttpGet("{folderID}/{ImageId}")]
        public ActionResult<string> Get(int folderID, string ImageId)
        {
            Byte[] imageBytes = null;
            string imageDataAddress = @"C:\E Drive\Precipoint Coding Chellange\openseadragon-bin-2.4.0\openseadragon\dzc_output_files\";

            nlogger.Information("Request has arrived to GET method ");   
          
            try
            {
                nlogger.Information("start Checking if the file exist ");
                if (System.IO.File.Exists(imageDataAddress + folderID.ToString() + "\\" + ImageId))
                {
                    nlogger.Information("File found : started reading the file");
                    imageBytes = System.IO.File.ReadAllBytes(imageDataAddress + folderID.ToString() + "\\" + ImageId);

                }

                nlogger.Debug("Returning File");
                return File(imageBytes, "image/jpeg");
            }
            
                catch (Exception ex)
            {
                //Save the logs in bin\Debug\netcoreapp2.1\ImageReaderLogs
                nlogger.Error("File not found exception has been raised :catch block ");
                throw new NotFoundCutomizedException("No Image found", $"Please check your folder path and " +
                    $"respective folder for  folderID:{folderID} and ImageId:{ImageId}");
            }
        
        }




        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
