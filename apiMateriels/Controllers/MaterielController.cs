using BLL.interfaces;
using BLL.security;
using domain.DTO;
using domain.DTO.Requests;
using domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace apiMateriels.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterielsController : ControllerBase
{
    private readonly IGestMaterielsService _gestMaterielsService;
    private readonly IsecurityService _securityService;

    public MaterielsController(IGestMaterielsService gestMaterielsService, IsecurityService securityService)

    {
        _securityService = securityService;
        _gestMaterielsService = gestMaterielsService;

    }

    ////POST->Création materiel:
    #region post materiel
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMaterielAsync([FromBody] DTOmaterielRequest DTOmaterielRequest)
    {
        //verification:
        if (DTOmaterielRequest.Name != null && DTOmaterielRequest.serviceDat != null && DTOmaterielRequest.endGarantee != null)
        {//New device création with the materiel 's DTO request values:
            Materiel newDevice = new Materiel()
            {
                Name = DTOmaterielRequest.Name,
                ServiceDat = DTOmaterielRequest.serviceDat,
                EndGarantee = DTOmaterielRequest.endGarantee,
                ProprietaireId = DTOmaterielRequest.proprietaireId,
                categories = DTOmaterielRequest.categories.Select(reference => new Category() { Reference = reference }).ToList()
            };
            var res = await _gestMaterielsService.CreateMaterielAsync(newDevice);

            var rep = new DTOmaterielResponse
            {
                Id = res.Id,
                Name = res.Name,
                ServiceDat = res.ServiceDat,
                EndGarantee = res.EndGarantee,
                proprietaireId = res.ProprietaireId
            };
            return Ok(rep);
        }
        else
        {
            return BadRequest();
        }


    }

    #endregion
    ////PUT-> update meteriel:
    #region update materiel
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMaterielAsync([FromBody] DTOmaterielRequest DTOmaterielRequest, [FromRoute] int id)
    {
        //verification:
        if (DTOmaterielRequest.Name != null || DTOmaterielRequest.serviceDat != null || DTOmaterielRequest.endGarantee != null || DTOmaterielRequest.categories != null)
        {
            //New device values for the the updated materiel retrived with the materiel DTO request values:
            Materiel updateDevice = new()
            {
                Id = id,
                Name = DTOmaterielRequest.Name,
                ServiceDat = DTOmaterielRequest.serviceDat,
                EndGarantee = DTOmaterielRequest.endGarantee,
                ProprietaireId = DTOmaterielRequest.proprietaireId,
                lastUpdate = DTOmaterielRequest.LastUpdate,
                categories = DTOmaterielRequest.categories.Select(reference => new Category() { Reference = reference }).ToList()
            };

            var res = await _gestMaterielsService.UpdateMaterielAsync(updateDevice);

            return Ok(res);
        }
        else
        {
            return BadRequest("Matériel non mis à jour.");
        }
        #endregion


    }


    #region delete materiel
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMaterielAsync([FromRoute] int id)
    {
        if (id < 0)
        {
            return BadRequest("Le matériel à supprimer n'existe pas.");
        }
        if (await _gestMaterielsService.DeleteMaterielAsync(id))
        {
            return Ok($"materiel{id} supprimé.");
        }
        else return BadRequest("Suppression non effectuée.");
    }

    #endregion
    //// GET->Read:
    #region get materiel by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMateriel([FromRoute] int id)
    {
        var materiel = await _gestMaterielsService.GetMaterielByIdAsync(id);
        if (materiel is not null)
        {
            return Ok(materiel);
        }
        else
        {
            return NotFound($"Le matériel {id} n'existe pas.");
        }
    }

    #endregion

    #region getAllMateriels
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> getAllMaterielsAsync()
    {
        return Ok(await _gestMaterielsService.GetAllMaterielsAsync());
    }
    #endregion
    #region category
    [HttpGet]
    [Route("categories")]
    public async Task<IActionResult> getAllCategoriesAsync()
    {
        return Ok(await _gestMaterielsService.GetAllCategoriesAsync());
    }




    #endregion
    [HttpGet]
    [Route("category/{reference}")]
    public async Task<IEnumerable<Materiel>> GetMaterielsByReference([FromRoute] int reference)
    {
        return await _gestMaterielsService.GetMaterielsByReference(reference);
    }
    [HttpGet]
    [Route("users")]
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _gestMaterielsService.GetAllUsersAsync();
    }
    [HttpGet]
    [Route("search")]
    public async Task<IEnumerable<Materiel>> GetMaterielByKeyValueAsync([FromQuery]string
         keyValue)
    {
        return await _gestMaterielsService.GetMaterielByKeyValueAsync(keyValue);
    }
    [HttpGet]
    [Route("categories/{idMat}")]
    public async Task<IEnumerable<Category>> GetCategoryWhereMateriel([FromRoute] int idMat)
    {
        return await _gestMaterielsService.GetCategoryWhereMateriel(idMat);
    }
}