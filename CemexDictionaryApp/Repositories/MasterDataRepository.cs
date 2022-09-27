using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public class MasterDataRepository
    {
        DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;
        public MasterDataRepository(DBContext _context, IWebHostEnvironment _hostEnvironment)
        {
            context = _context;
            hostEnvironment = _hostEnvironment;
        }

        public List<State> StateList ()
        {
            //List<State> _statelist = context.States.ToList();
           // return _statelist;

            List<State> Questions = context.States.Include(question => question.Zones).ToList();
            return Questions;
        }

        public List<Occupation> OccupationList()
        {
            //List<State> _statelist = context.States.ToList();
            // return _statelist;

            List<Occupation> _occupationList = context.Occupations.ToList();
            return _occupationList;
        }

    }
}
