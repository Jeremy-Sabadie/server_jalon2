using domain.Entities;

namespace BLL.interfaces;

public interface IGestMaterielsService
{
    // CRUD for Materiels:

    /// <summary>
    /// The GetAllMaterielsAsync méthod retrieves all existing materiel object.
    /// </summary>
    /// <returns>The list of all the materials</returns>
    public Task<IEnumerable<Materiel>> GetAllMaterielsAsync();
    public Task<IEnumerable<Materiel>> GetMaterielsByReference(int reference);
    public Task<Materiel> CreateMaterielAsync(Materiel materiel);
    /// <summary>
    /// Méthod GetMaterielByIdAsync is an Asynchrone méthod that retrieve a Matérial oject by his id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A Matérial object where id correspond.</returns>
    public Task<Materiel> GetMaterielByIdAsync(int id);
    public Task<Materiel> UpdateMaterielAsync(Materiel materiel);
    public Task<bool> DeleteMaterielAsync(int id);

    // CRUD for Category:
    //public Task<Category> CreateNewCategoryAsync(Category categogy);
    public Task<IEnumerable<Category>> GetAllCategoriesAsync();
    public Task<Category> GetCategoryByReference(int reference);
    Task<IEnumerable<User>> GetAllUsersAsync();
    public Task<IEnumerable<Materiel>> GetMaterielByKeyValueAsync(string keyValue);
    public Task<IEnumerable<Category>> GetCategoryWhereMateriel(int idMat);
}
