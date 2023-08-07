namespace ReportService.Api.Core.Constants
{
    public static class ProjectConst
    {
        public const string SendQueueReportList = "Rapor verileri kuyruğa iletildi.";
        public const string ReportRequestCompleted = "Rapor oluşturuldu. ReportDetail tablosuna kayıt işlemi yapıldı.";
        public const string ReportRequestRecived = "Rapor talebi alındı.";

        public const string AddLogMessage = "'{0}' tablosuna kayıt işlemi yapıldı.";
        public const string DeleteLogMessage = "'{0}' tablosunda silme işlemi yapıldı.";
        public const string UpdateLogMessage = "'{0}' tablosunda güncelleme işlemi yapıldı.";
    }
}
