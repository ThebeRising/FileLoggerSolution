
using FileLoggerKata;
using NUnit.Framework;
using System;
using System.IO;

namespace FileLoggerTests
{
    public class FileLoggerTests
    {
        
        FileSystem IFSI;
        FileLoggerKata.FileLogger fl;
        
        [SetUp]
        public void Setup()
        {
            IFSI = new FileSystem();
            fl = new FileLoggerKata.FileLogger(IFSI,new LogDateProvider());
            if (File.Exists("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt"))
            {
                File.Delete("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt");
            }

            if (File.Exists("weekend.txt"))
            {
                File.Delete("weekend.txt");
            }
        }

        [Test]
        public void TestWeekdayLogDoesNotExist()
        {
            Assert.IsFalse(File.Exists("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt"));
        }

        [Test]
        public void Test1stLogFileCreated()
        {
            Assert.IsFalse(File.Exists("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt"));

            fl.Log("New Log file!");
            
            Assert.IsTrue(File.Exists("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt"));
        }

        [Test]
        public void TestIfWeekdayLogCreatedWithCorrectName()
        {
           
            fl.Log("HI Weekday!");

            Assert.IsTrue(File.Exists("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt"));
            Assert.AreEqual("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt", Path.GetDirectoryName("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt"));

        }

        [Test]
        public void CheckSaturdayLogCreated()
        {
            fl = new FileLogger(new FileSystem(), new SaturdayDateProvider());

            fl.Log("Hi Saturday Weekend!");

            Assert.IsTrue(File.Exists("weekend.txt"));

        }

        [Test]
        public void TestSundayLogCreatedWhenLogCreatedOnSaturday()
        {
            fl = new FileLogger(new FileSystem(), new SaturdayDateProvider());

            fl.Log("Hi Saturday Weekend!");

            fl = new FileLogger(new FileSystem(), new SundayDateProvider());

            fl.Log("Hi Sunday Weekend!");

            Assert.IsTrue(File.Exists("weekend.txt"));
            Assert.IsTrue(File.ReadAllText("weekend.txt").Contains("Hi Sunday Weekend!"));
            Assert.IsTrue(File.ReadAllText("weekend.txt").Contains("Hi Saturday Weekend!"));

        }

        [Test]
        public void TestWeekendArchivedWhenNewWeekendLogCreated()
        {

            fl = new FileLogger(new FileSystem(), new ArchiveDateProvider());
            fl.Log("Hi Weekend setup!");

            fl = new FileLogger(new FileSystem(), new SaturdayDateProvider());
            fl.Log("Weekend is archived");


            Assert.IsTrue(File.Exists("weekend-" + new ArchiveDateProvider().Today.ToString("yyyyMMdd") + ".txt"));
        }


        [Test]
        public void CheckLogFormatOfLoggedText()
        {
            fl = new FileLogger(new FileSystem(), new LogDateProvider());

            fl.Log("Check");
            string loggedTextExpected = "2021-02-22 22:22:22 Check";

            Assert.AreEqual(loggedTextExpected, File.ReadAllText("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt"));

            if (File.Exists("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt"))
                File.Delete("Log" + new LogDateProvider().Today.ToString("yyyyMMdd") + ".txt");
        }

    }

    
}
