namespace csharp
{
    public static partial class Guard
    {
        public static void ThatItemQualityDoesNotExceedMax(Item item)
        {
            if (item.Quality > 50)
                item.Quality = 50;
        }
    }
}
