using System.Reflection;
using HtmlAgilityPack;

namespace Itis.MyTrainings.Api.Core.Managers;

/// <summary>
/// Менеджер для работы с Html файлами
/// </summary>
public static class HtmlFileManager
{
    /// <summary>
    /// Получить тело из html файла
    /// </summary>
    /// <param name="htmlFilePath">Путь до html файла</param>
    /// <param name="fileName">Имя файла</param>
    /// <returns></returns>
    public static string GetHtmlFileBody(string htmlFilePath, string fileName)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{htmlFilePath}.{fileName}")
            ?? throw new FileNotFoundException($"Файл с названием {fileName} не найден");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Получить заголовок из текста html
    /// </summary>
    /// <param name="htmlText">Текст html</param>
    /// <returns></returns>
    public static string? GetTitle(string htmlText)
    {
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(htmlText);

        HtmlNode titleNode = htmlDoc.DocumentNode.SelectSingleNode("//title");

        return titleNode.InnerText;
    }
}