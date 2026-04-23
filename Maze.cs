// Massimo Lovjoy April 21st 2026, Maze

using System.Diagnostics;

Console.WriteLine("Welcome to the maze game! You will have to navigte the dark and dangerous maze to find the *! Use the arrow keys to move and escape to quit.");
int cursorX = 0;
int cursorY = 0;
string[] mapRows;
bool victory = false;
Stopwatch time = new Stopwatch ();
Console.WriteLine("Press any key to start.");
Console.ReadKey();
main();

void main() {
    /* initialisation */
    mapRows = File.ReadAllLines("map.txt");
    Console.Clear();
    foreach (string row in mapRows) {
        Console.WriteLine(row);
    }
    placeCursor(cursorX, cursorY);
    /* play */
    bool endGame = false;
    time.Start();
    do {
        endGame = runGame();
        if (endGame == false){placeCursor(cursorX, cursorY);}
        if (victory == true) {break;}
        } while (endGame == false);
    if (endGame == true) {
        placeCursor(0, 6);
        Console.WriteLine("You lost or quit! Don't hit the #!");
    } 
}
void placeCursor(int x,int y){
    // Console.WriteLine(Console.GetCursorPosition());
    Console.CursorTop = y;
    Console.CursorLeft = x;
}
bool runGame(){
    switch (Console.ReadKey(true).Key) 
    {
        case ConsoleKey.Escape:
            return true;
        case ConsoleKey.RightArrow:
            cursorX += 1;
            return checkFail(cursorX, cursorY);
        case ConsoleKey.LeftArrow:
            cursorX -= 1;
            return checkFail(cursorX, cursorY);
        case ConsoleKey.UpArrow:
            cursorY -= 1;
            return checkFail(cursorX, cursorY);
        case ConsoleKey.DownArrow:
            cursorY += 1;
            return checkFail(cursorX, cursorY);
        default:
            return false;
    }
}
bool checkFail(int x, int y){ 
    if (x < 0 || y < 0 || y >= mapRows.Length || x >= mapRows[y].Length) {
        return true;
    } else {
        char mapChar = mapRows[y][x];
        if (mapChar == "#"[0]) {
            return true;
        } else if (mapChar == "*"[0]) {
            gameEnd();
            return false;
        } else {
            return false;
        }
    }
    // Console.WriteLine($"mY: {mapRows.Length} Y: {y}, mX: {mapRows[y].Length}");
}
void gameEnd(){
    Console.Clear();
    victory = true;
    Console.WriteLine($"Congratulations! You beat the maze in {(double)(time.ElapsedMilliseconds)/1000} seconds!");
}
