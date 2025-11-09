public sealed class Rating
{
    public int Bullet { get; private set; }
    public int BulletK { get; private set; }
    public int Blitz { get; private set; }
    public int BlitzK { get; private set; }
    public int Rapid { get; private set; }
    public int RapidK { get; private set; }
    public int Classical { get; private set; }
    public int ClassicalK { get; private set; }

    public Rating(int bullet, int bulletK, int blitz, int blitzK, int rapid, int rapidK, int classical, int classicalK)
    {
        Bullet = bullet;
        BulletK = bulletK;
        Blitz = blitz;
        BlitzK = blitzK;
        Rapid = rapid;
        RapidK = rapidK;
        Classical = classical;
        ClassicalK = classicalK;
    }

    public void UpdateBullet(int newRating, int newK)
    {
        Bullet = newRating;
        BulletK = newK;
    }
    public void UpdateBlitz(int newRating, int newK)
    {
        Blitz = newRating;
        BlitzK = newK;
    }
    public void UpdateRapid(int newRating, int newK)
    {
        Rapid = newRating;
        RapidK = newK;
    }
    public void UpdateClassical(int newRating, int newK)
    {
        Classical = newRating;
        ClassicalK = newK;
    }
}