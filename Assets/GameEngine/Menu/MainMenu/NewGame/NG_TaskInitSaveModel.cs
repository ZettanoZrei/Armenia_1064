namespace Loader
{
    class NG_TaskInitSaveModel : ING_Task
    {
        private readonly Repository repository;

        public NG_TaskInitSaveModel(Repository repository)
        {
            this.repository = repository;
        }
        void ING_Task.Execute()
        {
            repository.InitNewSaveData();
        }
    }
}
