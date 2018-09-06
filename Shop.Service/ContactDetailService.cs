using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
    public interface IContactDetailService
    {
        ContactDetail Create(ContactDetail error);
        void Update(ContactDetail contactDetail);
        ContactDetail Delete(int id);
        IEnumerable<ContactDetail> GetAllPaging(int page, int pageSize, out int totalRow);
        ContactDetail GetById(int id);
        void save();
    }
    public class ContactDetailService : IContactDetailService
    {
        IContactDetailRepository _contactDetailRepository;
        IUnitOfWork _unitOfWork;
        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._contactDetailRepository = contactDetailRepository;
            this._unitOfWork = unitOfWork;
        }
        public ContactDetail Create(ContactDetail error)
        {
            return _contactDetailRepository.Add(error);
        }

        public ContactDetail Delete(int id)
        {
            return _contactDetailRepository.Delete(id);
        }

        public IEnumerable<ContactDetail> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            var result = _contactDetailRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
            return result;
        }

        public ContactDetail GetById(int id)
        {
            return _contactDetailRepository.GetSingleById(id);
        }

        public void save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ContactDetail contactDetail)
        {
            _contactDetailRepository.Update(contactDetail);
        }
    }
}
