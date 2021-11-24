using GestionContactEF.Core.Interfaces;
using GestionContactEF.Dal.DBContext;
using GestionContactEF.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Core.Repositories
{
    public class AdresseRepository : Repository<int, Adresse>, IAdresseRepository
    {
        private readonly Context _context;
        private readonly DbSet<Adresse> _entities;
        public AdresseRepository(Context context) : base(context)
        {
            _context = context;
            _entities = _context.Set<Adresse>();
        }

        public async Task<bool> AlreadyExists(string rue, string ville, int CP, int numero, int? boite)
        {
            if(!(boite != 0))
            {
                if (await _context.Adresses
                .Where(x => x.Rue.Equals(rue))
                .Where(x => x.Ville.Equals(ville))
                .Where(x => x.CP.Equals(CP))
                .Where(x => x.Numero.Equals(numero))
                .Where(x => x.Boite.Equals(boite))
                .AnyAsync())
                    return true;
                return false;
            }
            else if (await _context.Adresses
                .Where(x => x.Rue.Equals(rue))
                .Where(x => x.Ville.Equals(ville))
                .Where(x => x.CP.Equals(CP))
                .Where(x => x.Numero.Equals(numero))
                .AnyAsync())
                return true;
            return false;
        }

        public async Task<IEnumerable<Adresse>> Get(bool lazyloading, string rue, string ville, int CP, int numero, int? boite)
        {
            var request = from adresses in _context.Adresses select adresses;
            switch (lazyloading)
            {
                case true:
                    if (!(rue is null))
                    {
                        request = request.OrderBy(w => w.Rue)
                            .Include(w => w.Contacts)
                            .ThenInclude(w => w.User)
                            .Where(w => w.Rue.Contains(rue));
                    }
                    else if (!(ville is null))
                    {
                        request = request.OrderBy(w => w.Ville)
                            .Include(w => w.Contacts)
                            .ThenInclude(w => w.User)
                            .Where(w => w.Ville.Contains(ville));
                    }
                    else if (!(ville is null) && !(rue is null) && !(numero == 0) && !(CP == 0))
                    {
                        request = request.OrderBy(w => w.Ville)
                            .Include(w => w.Contacts)
                            .ThenInclude(w => w.User)
                            .Where(w => w.Ville.Contains(ville))
                            .Where(w => w.Rue.Contains(rue))
                            .Where(w => w.Numero == numero)
                            .Where(w => w.CP == CP);
                    }
                    else if (!(ville is null) && !(rue is null) && !(numero == 0) && !(CP == 0) && !(boite == 0))
                    {
                        request = request.OrderBy(w => w.Ville)
                            .Include(w => w.Contacts)
                            .ThenInclude(w => w.User)
                            .Where(w => w.Ville.Contains(ville))
                            .Where(w => w.Rue.Contains(rue))
                            .Where(w => w.Numero == numero)
                            .Where(w => w.CP == CP)
                            .Where(w => w.Boite == boite);
                    }
                    else
                    {
                        request = request.Include(w => w.Contacts)
                            .ThenInclude(w => w.User)
                            .OrderBy(w => w.Ville);
                    }
                    break;

                case false:
                    if (!(rue is null))
                    {
                        request = request.OrderBy(w => w.Rue)
                            .Where(w => w.Rue.Contains(rue));
                    }
                    else if (!(ville is null))
                    {
                        request = request.OrderBy(w => w.Ville)
                            .Where(w => w.Ville.Contains(ville));
                    }
                    else
                    {
                        request = request.OrderBy(w => w.Ville);
                    }
                    break;
            }
            return await request.ToListAsync();
        }

        public async Task<Adresse> GetOne(int id, bool lazyLoading)
        {
            if (lazyLoading)
            {
                return await _entities
                    .Include(e => e.Contacts)
                    .Where(w => w.Id == id)
                    .FirstOrDefaultAsync();
            }
            return await _entities.FirstAsync();
        }

        public async Task<Adresse> GetOne(string rue, string ville, int CP, int numero, int? boite)
        {
            if(boite is null)
            {
                return await _entities
                .Where(x => x.Numero == numero)
                .Where(x => x.Ville.Equals(ville))
                .Where(x => x.CP == CP)
                .Where(x => x.Rue.Equals(rue)).FirstAsync();
            }
            else
            {
                return await _entities
                .Where(x => x.Numero == numero)
                .Where(x => x.Ville.Equals(ville))
                .Where(x => x.CP == CP)
                .Where(x => x.Rue.Equals(rue))
                .Where(x => x.Boite == boite).FirstAsync();
            }
            
        }
    }
}
