﻿using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkerRepository
    {
        List<Walker> GetAllWalkers();
        Walker GetWalkerById(int id);
        void AddWalker(Walker walker);
        void DeleteWalker(int id);
        void UpdateWalker(Walker walker);
    }
}
