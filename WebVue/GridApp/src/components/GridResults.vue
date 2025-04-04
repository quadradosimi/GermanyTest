<script setup lang="ts">
  import type { GridApi, GridOptions } from "ag-grid-community";
  import {
    AllCommunityModule,
    ModuleRegistry,
    createGrid,
  } from "ag-grid-community";
  import service from "./Services.vue";

  ModuleRegistry.registerModules([AllCommunityModule]);

  // Row Data Interface
  interface IRow {
    year: string;
    description: string;
    id: number;
  }

  // Access to Grid
  let gridApi: GridApi;

  // grid configurations
  const gridOptions: GridOptions<IRow> = {
    // Data
    rowData: [],
    // Columns
    columnDefs: [
      { field: "year", filter: "agSetColumnFilter"  },
      { field: "description", filter: "agSetColumnFilter"  },
    ],
    defaultColDef: {
      flex: 1,
    },
  };

  // Create new grid
  gridApi = createGrid(
    document.querySelector<HTMLElement>("#myGrid")!,
    gridOptions,
  );

  function dataBaseRowData() {
    service.setGridWithDbData(gridApi);
  }

  //set data to grid
  dataBaseRowData();
</script>
