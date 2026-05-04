using System;

namespace Shared;

public class Functions
{
      public static string FormatSize(long bytes)
      {
            if (bytes == 0) return "0 B";
            string[] sizes = ["B", "KB", "MB", "GB"];

            int order = 0;
            int k = 1024;
            double size = bytes;

            while (size >= k && order < sizes.Length)
            {
                  size /= k;
                  order++;
            }
            return $"{size:F2} {sizes[order]}";
      }

      public static string ShortingString(string item, int index)
      {
            return !string.IsNullOrEmpty(item) && item.Length > index ? item[..index] + "..." : item;
      }

      public static string GetFileIconClass(string contentType) => contentType switch
      {
            var t when t.StartsWith("image/") => "image",
            "application/pdf" => "pdf",
            _ => "doc"
      };

      public static string GetFileIcon(string contentType) => contentType switch
      {
            var t when t.StartsWith("image/") => "fa-file-image",
            "application/pdf" => "fa-file-pdf",
            var t when t.Contains("word") || t.Contains("document") => "fa-file-word",
            var t when t.Contains("excel") || t.Contains("sheet") => "fa-file-excel",
            _ => "fa-file"
      };
    
}
