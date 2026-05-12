using Application.IRepositories.MySql;
using Microsoft.EntityFrameworkCore;
using MySqlDomain;
using MySqlPersistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlPersistence.Repositories;
public class RealGameEventRepository : MySqlBaseRepository<RealGameEvent>, IRealGameEventRepository {
    private readonly MySqlContext _context;
    public RealGameEventRepository(MySqlContext context) : base(context) {
        _context = context;
    }
    public async Task<List<RealGameEvent>> GetByFechaOperacionCursor(DateTime fechaOperacion) {
        DateTime fechaInicio = fechaOperacion.Date;
        DateTime fechaFinal = fechaOperacion.Date.AddDays(1);

        var resultado = new List<RealGameEvent>();

        DateTime? lastDate = fechaInicio;
        ulong lastId = 0;

        const int batchSize = 5000;

        while(true) {
            var data = await _context.RealGameEvents
                .FromSql($@"
                SELECT 
                    cast(`eventId` as bigint) as eventId,
                    cast(`storeId` as char(30)) as storeId,
                    cast(`playerId` as innt) as playerId,
                    cast(`providerId`as int) as providerId,
                    cast(`gameId` as  char(60)) as gameId,
                    cast(`type` as char(16)) as type,
                    cast(`amount` as bigint) as amount,
                    cast(`status` as tinyiny) as status,
                    cast(`insDatetime` as datetime) as insDatetime,
                    cast(`insTimestamp` as bigint) as insTimestamp,
                    cast(`coinsType` as char(20)) as coinsType,
                    cast(`associateId` as char(11)) as associateId
                FROM `RealGameEvents`
                WHERE 
                    (insDatetime > {lastDate}
                     OR (insDatetime = {lastDate} AND eventId > {lastId}))
                    AND insDatetime < {fechaFinal}
                ORDER BY insDatetime, eventId
                LIMIT {batchSize}
            ")
                .AsNoTracking()
                .ToListAsync();

            if(data.Count == 0)
                break;

            resultado.AddRange(data);

            var lastItem = data.Last();
            lastDate = lastItem.InsDatetime;
            lastId = lastItem.EventId;
        }

        return resultado;
    }
    public async Task<IEnumerable<RealGameEvent>> GetByFechaOperacion(DateTime fechaOperacion) {
        DateTime fechaInicio = fechaOperacion.Date;
        DateTime fechaFinal = fechaOperacion.Date.AddDays(1);
        FormattableString query = @$"
            with `TemporaryRealGameEvents` AS (
                select `eventId`,
                `storeId`,
                `playerId`,
                `providerId`,
                `gameId`,
                `type`,
                `amount`,
                `status`,
                `insDatetime`,
                `insTimestamp`,
                `coinsType`,
                `associateId`
                FROM `RealGameEvents`
                where insDatetime >= {fechaInicio} and insDatetime < {fechaFinal}
                )
                SELECT * FROM `TemporaryRealGameEvents`
    ";
        _context.Database.SetCommandTimeout(600);//10 minutos
        return await _context.RealGameEvents.FromSql(query).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<RealGameEvent>> GetPaginatedByDates(int page, int pageSize, DateTime insDateTime) {
        return await _context.RealGameEvents.Where(x => x.InsDatetime >= insDateTime).OrderBy(x=>x.InsDatetime).Skip(page).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetTotalRecordsByDate(DateTime insDateTime) {
        return await _context.RealGameEvents.Where(x=>x.InsDatetime >= insDateTime).CountAsync();
    }

    public async Task<IEnumerable<RealGameEvent>> GetPaginatedById(int page, int pageSize, ulong id) {
        return await _context.RealGameEvents.Where(x=>x.EventId >= id).OrderBy(x=>x.EventId).Skip(page).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetTotalRecordsById(ulong id) {
        return await _context.RealGameEvents.Where(x => x.EventId >= id).CountAsync();
    }
}
