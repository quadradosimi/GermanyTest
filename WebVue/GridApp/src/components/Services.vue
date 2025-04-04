<script lang="ts">

  import axios from 'axios';
  import type { GridApi} from "ag-grid-community";
  import config from "../../config.json";

  export default{
    setGridWithDbData (gridApi:GridApi) {
      let url = config.SERVER_URL;

      axios.get(url)
      .then(function (response) {
        let dados = response.data;

        gridApi.setGridOption("rowData", dados);
      }) 
      .catch(function (error) {
        console.log(error)
      })
    }
} 
</script>