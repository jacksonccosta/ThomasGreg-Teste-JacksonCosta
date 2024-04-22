using System.ComponentModel.DataAnnotations;

namespace ThomasGregTest.WebApp;

public class ValidarImagemAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        IFormFile? file = value as IFormFile;

        if (file == null)
            return ValidationResult.Success;

        var allowedExtensions = new[] { ".jpeg", ".jpg", ".png"};

        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
            return new ValidationResult("Tipo de Imagem não permitida.");

        return ValidationResult.Success;
    }
}
