using BLL.interfaces;
using DAL.UOW;
using domain.Entities;

namespace BLL.services;

internal class GestMaterielService : IGestMaterielsService
{
    private readonly IUOW _db;

    public GestMaterielService(IUOW db)
    {
        _db = db;
    }

    public async Task<Materiel> CreateMaterielAsync(Materiel materiel)
    {
        var newMateriel = await _db.Materiels.CreateMaterielAsync(materiel);
        return newMateriel;
    }
    public async Task<Materiel> GetMaterielByIdAsync(int id)
    {
        return await _db.Materiels.GetMaterielByIdAsync(id);
    }
    public Task<Category> CreateNewCategoryAsync(Category categogy)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _db.Categories.GetAllCategories();

    }
    public async Task<Category> GetCategoryByReference(int reference)
    {

        return await _db.Categories.GetCategoryByReference(reference);
    }

    public async Task<IEnumerable<Materiel>> GetAllMaterielsAsync()
    {
        return await _db.Materiels.GetAllMaterielsAsync();
    }
    public Task<IEnumerable<Materiel>> GetMaterielsByReference(int reference)
    {
        return _db.Materiels.GetMaterielsByCategory(reference);
    }

    public async Task<Materiel> UpdateMaterielAsync(Materiel materiel)
    {
        var updated = await _db.Materiels.UpdateMaterielAsync(materiel);
        return updated;
    }
    public async Task<bool> DeleteMaterielAsync(int id)
    {
        _db.BeginTransaction();
        var isDeleted = await _db.Materiels.DeleteMaterielAsync(id);
        _db.Commit();

        return isDeleted;
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return (IEnumerable<User>)await _db.Users.GetAllUsersAsync();
    }
    public async Task<IEnumerable<Materiel>> GetMaterielByKeyValueAsync(string keyValue)
    {
        return await _db.Materiels.GetMaterielByKeyValueAsync(keyValue);
    }
    public async Task<IEnumerable<Category>> GetCategoryWhereMateriel(int idMat)
    {
        return await _db.Materiels.GetCategoryWhereMateriel(idMat);
    }
}
