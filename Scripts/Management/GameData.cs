public static class GameData
{
    // Maze Level Tags
    public static string [] mazes = {"The Green Maze", "The Yellow Maze", "The Orange Maze", "The Blue Maze", "The Red Maze"};
    
    // Forest Item Tags
    public static string [] materials = {"Pinecone", "Coal", "Log"};

    public static int currentMazeIndex = 0;                                 // Index of the maze the player is currently exploring

    public static float totalTimeAccumulated = 0f;                          // Total amount of extra maze time player has accumulated
    public static float playerLight = -1f;                                  // Current light intensity of player character

    public static void ResetGame()
    {
        // Reset variables to initial values
        currentMazeIndex = 0;
        totalTimeAccumulated = 0f;
        playerLight = -1f;
    }
}