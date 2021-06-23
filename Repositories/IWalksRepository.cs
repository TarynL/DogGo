using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalksRepository
    {
        List<Walks> GetAllWalks();
        List<Walks> GetAllWalksByWalkerId(int id);

        void AddWalk(Walks walk);
    }
}