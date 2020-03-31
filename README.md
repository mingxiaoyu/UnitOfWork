![build&Test](https://github.com/mingxiaoyu/UnitOfWork/workflows/build&Test/badge.svg)

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
 
 ### DBContext
 ```csharp
 public class BloggingContext : DbContextBase
 ``` 
 ### Table 
        The entity extends  EntityTypeConfiguration, it will auto apply into dbcontext as table
  ```csharp
 public class Blog : TrackedAndSoftDelete
    {
        public string Url { get; set; }
    }

    public class BlogMap : EntityTypeConfiguration<Blog>
    {
        public override void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs");
            builder.Property(x => x.Url).HasMaxLength(100);
            base.Configure(builder);
        }
    }
 ``` 
 ### View  
        The entity extends  QueryTypeConfiguration, it will auto apply into dbcontext as table
   ```csharp
  public class BlogsView
    {
        public string Url { get; set; }
    }

    public class BlogsViewMap : QueryTypeConfiguration<BlogsView>
    {
        public override void Configure(QueryTypeBuilder<BlogsView> builder)
        {
            builder.ToView("BolgViews");
            base.Configure(builder);
        }
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
