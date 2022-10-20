public readonly struct LevelTimeData
{
    private readonly float elapsed;
    private readonly float remaining;
    private readonly float bonus;

    public LevelTimeData(float elapsed, float remaining, float bonus)
    {
        this.elapsed = elapsed;
        this.remaining = remaining;
        this.bonus = bonus;
    }

    public float Elapsed { get => elapsed; }
    public float Remaining { get => remaining; }
    public float Bonus { get => bonus; }
}
