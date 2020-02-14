using AutoMapper;
using DonVo.Persistences;
using DonVo.Services.Interfaces.Directories;
using DonVo.SystemAudit.Repository;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DonVo.nUnitTests
{
    public class TestsBase
    {
        protected readonly AutoMapperFactory _autoMapper = new AutoMapperFactory();
        protected Mock<ISystemAuditRepository> _mockedRepository;
        protected Mock<ICurrencyService> _currencyService;
        protected  ContosoRetailDWContext _context;
        //protected  Mock<IMapper> _mapper;

        [SetUp]
        public void SetupBase()
        {
            _mockedRepository = new Mock<ISystemAuditRepository>();

            _currencyService = new Mock<ICurrencyService>();
            _context = new ContosoRetailDWContext();
            //_mapper = new Mock<IMapper>();

        }
    }
}
