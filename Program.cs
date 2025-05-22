using System;
using System.Collections.Generic;
using System.IO;


//какие то загадочные типы, необходимые по условию
public class MyData1
{
    public int Data { get; set; }
}
public class MyData2
{
    public string Data { get; set; }
}

public abstract class File
{
    public abstract StreamReader Open();
    public abstract StreamReader Close(StreamReader lines);
    public abstract void Seek(StreamReader lines);
    public abstract void Write(ref StreamReader lines);
    public abstract void GetPosition();
    public abstract void GetLength(StreamReader lines);
}

public class MyDataFile1 : File
{
    public string nameOfFile { get; set; }
    public MyData1 data { get; set; }
    public string path_to_file { get; set; }

    public MyDataFile1(string nameOfFile, string path_to_file, MyData1 data)
    {
        this.nameOfFile = nameOfFile;
        this.path_to_file = path_to_file;
        this.data = data;
    }




    public override StreamReader Open()
    {
        Console.WriteLine("============(Файл типа 1 открыт)============");
        return new StreamReader(path_to_file);
    }
    public override StreamReader Close(StreamReader lines)
    {
        Console.WriteLine("============(Файл типа 1 закрыт)============");
        lines.Close();
        return null;
    }
    public override void Seek(StreamReader lines)
    {
        Console.WriteLine("Введите, что желаете найти?");
        string wanted = Console.ReadLine();
        string line = lines.ReadLine();
        int countLine = 1;
        while (notEndOfLines(line))
        {
            if (line.Contains(wanted))
            {
                Console.WriteLine($"Нашлось в {countLine} строке");
            }
            countLine++;
            line = lines.ReadLine();
        }
        Console.WriteLine("Больше в файле не найдено");
    }
    public override void Write(ref StreamReader lines)
    {
        lines.Close();
        StreamWriter writer = new StreamWriter(path_to_file);
        Console.WriteLine("Введите текст, который хотите добавить в файл");
        Console.WriteLine("***Напечатайте 0, когда захотите закончить текст для записи***");
        string userText = Console.ReadLine();
        while (userWriting(userText))
        {
            writer.WriteLine(userText);
            userText = Console.ReadLine();
        }
        writer.Close();
        Console.WriteLine("Текст записан в файл");
        lines = new StreamReader(path_to_file);
    }
    public override void GetPosition()
    {
        Console.WriteLine(path_to_file);
    }
    public override void GetLength(StreamReader lines)
    {
        int countLine = 0;
        while (lines.ReadLine() != null)
        {
            countLine++;
        }
        Console.WriteLine($"{countLine} строк");
    }

    private bool notEndOfLines(string line)
    {
        return line != null;
    }
    private bool userWriting(string Text)
    {
        return Text != "0";
    }
}
public class MyDataFile2 : File
{
    public string nameOfFile { get; set; }
    public MyData2 data { get; set; }
    public string path_to_file { get; set; }

    public MyDataFile2(string nameOfFile, string path_to_file, MyData2 data)
    {
        this.nameOfFile = nameOfFile;
        this.path_to_file = path_to_file;
        this.data = data;
    }

    public override StreamReader Open()
    {
        Console.WriteLine("============(Файл типа 1 открыт)============");
        return new StreamReader(path_to_file);
    }
    public override StreamReader Close(StreamReader lines)
    {
        Console.WriteLine("============(Файл типа 1 закрыт)============");
        lines.Close();
        return null;
    }
    public override void Seek(StreamReader lines)
    {
        Console.WriteLine("Введите, что желаете найти?");
        string wanted = Console.ReadLine();
        string line = lines.ReadLine();
        int countLine = 1;
        while (notEndOfLines(line))
        {
            if (line.Contains(wanted))
            {
                Console.WriteLine($"Нашлось в {countLine} строке");
            }
            countLine++;
            line = lines.ReadLine();
        }
        Console.WriteLine("Больше в файле не найдено");
    }
    public override void Write(ref StreamReader lines)
    {
        lines.Close();
        StreamWriter writer = new StreamWriter(path_to_file);
        Console.WriteLine("Введите текст, который хотите добавить в файл");
        Console.WriteLine("***Напечатайте 0, когда захотите закончить текст для записи***");
        string userText = Console.ReadLine();
        while (userWriting(userText))
        {
            writer.WriteLine(userText);
            userText = Console.ReadLine();
        }
        writer.Close();
        Console.WriteLine("Текст записан в файл");
        lines = new StreamReader(path_to_file);
    }
    public override void GetPosition()
    {
        Console.WriteLine(path_to_file);
    }
    public override void GetLength(StreamReader lines)
    {
        int countLine = 0;
        while (lines.ReadLine() != null)
        {
            countLine++;
        }
        Console.WriteLine($"{countLine} строк");
    }

    private bool notEndOfLines(string line)
    {
        return line != null;
    }
    private bool userWriting(string Text)
    {
        return Text != "0";
    }
}

class Folder
{
    private List<MyDataFile1> files;
    private int currentFileIndex = -1;
    private StreamReader currentReader = null;
    public Folder()
    {
        files = new List<MyDataFile1>();
        //filesSR = new List<StreamReader>(); // Инициализация filesSR
    }

    public void GetList()
    {
        int c = 1;
        foreach (MyDataFile1 f in files)
        {
            Console.Write($"{c} {f.nameOfFile}       ");
            f.GetPosition();
            c++;
        }

    }

    public void AddFile(MyDataFile1 file)
    {

        files.Add(file);

    }
    public void RemoveFile(MyDataFile1 file)
    {
        if (files.Contains(file))
        {
            int index = files.IndexOf(file);
            files.Remove(file);
            Console.WriteLine($"Файл {file.nameOfFile} удален из папки.");
        }
        else
        {
            Console.WriteLine($"Файл {file.nameOfFile}не найден в папке.");
        }
    }


    public void menu()
    {
        while (true)
        {
            Console.WriteLine("Список файлов в папке:\nС каким файлом вы хотите взаимодействовать?");
            GetList();

            int indexOfCurrentFile = Convert.ToInt16(Console.ReadLine()) - 1;
            DoMethods(indexOfCurrentFile);

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void DoMethods(int indexOfCurrentFile)
    {
        if (indexOfCurrentFile < 0 || indexOfCurrentFile >= files.Count)
        {
            Console.WriteLine("Некорректный выбор файла.");
            return;
        }

        MyDataFile1 currentFile = files[indexOfCurrentFile];

        Console.WriteLine("Что вы хотите с ним сделать?");
        Console.WriteLine("1. Открыть файл\n2. Закрыть файл\n3. Найти в файле\n4. Записать в файл\n5. Получить местоположение файла\n6. Узнать длину файла\n7. Удалить файл\n8. Выход");

        int to_do = Convert.ToInt16(Console.ReadLine());

        try
        {
            switch (to_do)
            {
                case 1:
                    if (currentReader == null)
                    {
                        currentReader = currentFile.Open();
                        currentFileIndex = indexOfCurrentFile;
                    }
                    else
                    {
                        Console.WriteLine("Файл уже открыт. Закройте текущий файл перед открытием нового.");
                    }
                    break;
                case 2:
                    if (currentReader != null && indexOfCurrentFile == currentFileIndex)
                    {
                        currentReader = currentFile.Close(currentReader);
                        currentFileIndex = -1;
                    }
                    else
                    {
                        Console.WriteLine("Нет открытого файла для закрытия.");
                    }
                    break;
                case 3:
                    if (currentReader != null && indexOfCurrentFile == currentFileIndex)
                    {
                        currentFile.Seek(currentReader);
                    }
                    else
                    {
                        Console.WriteLine("Файл не открыт.");
                    }
                    break;
                case 4:
                    if (currentReader != null && indexOfCurrentFile == currentFileIndex)
                    {
                        currentFile.Write(ref currentReader);
                    }
                    else
                    {
                        Console.WriteLine("Файл не открыт.");
                    }
                    break;
                case 5:
                    currentFile.GetPosition();
                    break;
                case 6:
                    if (currentReader != null && indexOfCurrentFile == currentFileIndex)
                    {
                        currentFile.GetLength(currentReader);
                    }
                    else
                    {
                        Console.WriteLine("Файл не открыт.");
                    }
                    break;
                case 7:
                    RemoveFile(currentFile);
                    break;
                case 8:
                    Console.WriteLine("Выход из меню.");
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
class Program
{
    static void Main()
    {
        Folder folder = new Folder();
        MyData1 smth1 = new MyData1();
        MyData1 smth2 = new MyData1();
        MyData1 smth3 = new MyData1();
        MyDataFile1 file1 = new MyDataFile1("text1", "C:\\Users\\acer\\source\\repos\\ConsoleApp1_c#\\ConsoleApp1_c#\\text1.txt", smth1);
        MyDataFile1 file2 = new MyDataFile1("text2", "C:\\Users\\acer\\source\\repos\\ConsoleApp1_c#\\ConsoleApp1_c#\\text2.txt", smth2);
        MyDataFile1 file3 = new MyDataFile1("text3", "C:\\Users\\acer\\source\\repos\\ConsoleApp1_c#\\ConsoleApp1_c#\\text3.txt", smth3);

        folder.AddFile(file1);
        folder.AddFile(file2);
        folder.AddFile(file3);
        folder.menu();

    }
}
