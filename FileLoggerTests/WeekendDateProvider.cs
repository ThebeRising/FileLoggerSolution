using System;
using FileLoggerKata;

namespace FileLoggerTests
{
    public class SaturdayDateProvider : IDateProvider
    {
        public static IDateProvider Instance => new DefaultDataProvider();

        public DateTime Today => new DateTime(2022, 01, 29, 13, 30, 23);
    }

    public class SundayDateProvider : IDateProvider
    {
        public static IDateProvider Instance => new DefaultDataProvider();

        public DateTime Today => new DateTime(2022, 01, 30, 13, 30, 23);
    }

    public class ArchiveDateProvider : IDateProvider
    {
        public static IDateProvider Instance => new DefaultDataProvider();

        public DateTime Today => new DateTime(2022, 01, 22, 13, 30, 23);
    }

    public class LogDateProvider : IDateProvider
    {
        public static IDateProvider Instance => new DefaultDataProvider();

        public DateTime Today => new DateTime(2021, 02, 22, 20, 22, 22);
    }

    public class DifferentTimeLogDateProvider : IDateProvider
    {
        public static IDateProvider Instance => new DefaultDataProvider();

        public DateTime Today => new DateTime(2021, 02, 22, 22, 22, 22);
    }
}
