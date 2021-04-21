namespace OceanSurvival
{
    [System.Serializable]
    public struct ItemCount
    {
        public Item item;
        public int count;

        public ItemCount(Item _item, int _count)
        {
            item = _item;
            count = _count;
        }
    }
}
