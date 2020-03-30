![.NET Core](https://github.com/mingxiaoyu/UnitOfWork/workflows/.NET%20Core/badge.svg)

# Quickly start

## How to use UnitOfWork
```csharp
        services.AddUnitOfWork<BloggingContext>(setup =>
              setup.UseSqlServer(
                 "Data Source=(localdb)\\MSSqlLocalDb;Initial Catalog=UnitOfWorkDb;Integrated Security=true;MultipleActiveResultSets=true;"
             ), typeof(Startup).GetTypeInfo().Assembly);
             
        private readonly IRepository<Blog> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogsController(IRepository<Blog> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
 ```  

### Insert : 
```csharp
var user = _repository.Insert(new Blog() { Url = value });
_unitOfWork.Commit();
 ```    
 
### Update:
```csharp
var entity = _repository.Table.FirstOrDefault(x => x.Id == id);
entity.Url = value;
_repository.Update(entity);
 _unitOfWork.Commit();
```       
### Delete:
```csharp
var entity = _repository.Table.FirstOrDefault(x => x.Id == id);
_repository.Delete(entity);
```   
### Transaction:
```csharp
  using (var tran = _unitOfWork.BeginTransaction())
            {
                ......
                _unitOfWork.Commit();
                tran.Commit();
            }
```

### Query:
```csharp
var list = _repository.Table.ToList().Select(x => x.Url).ToArray();
_unitOfWork.Commit();
```   
        
### Paginate:
```csharp
var paging=   _repository.Table.ToPaginatedList(1,20);
```   
