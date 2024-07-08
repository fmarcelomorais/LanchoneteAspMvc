namespace LanchoneteAspMvc.Models
{
    public class FileManagerModel
    {
        public FileInfo[] Files { get; set; }
        public IFormFile IFormFile { get; set; }
        public List<IFormFile> IFormeFormFiles { get; set; }
        public string PathImageProdutos { get; set; }
    }
}
