namespace GirlsHandmadeShop.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(int productId, string userId, byte value);

        double GetAverageVotes(int productId);
    }
}
