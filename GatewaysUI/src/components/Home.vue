
<template>
    <div class="home">
        <div class="wrap">
            <div>
                <h2>Gateways</h2><button @click="form2=true, form=false">Add Gateway</button>
                <table id="table">
                    <thead>
                        <tr>
                            <th>Serial Number</th>
                            <th>Human Readable Name</th>
                            <th>IPv4</th>
                            <th>Buttons</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item,index) in gateways" :key="index">
                            <td>
                                {{ item.serialNumber }}
                            </td>
                            <td>{{item.hrname}}</td>
                            <td>{{item.ipv4}}</td>
                            <td><button @click="getPeripherals(item.serialNumber)">See Peripherals</button> <button @click="form=true,selectedSN=item.serialNumber,form2=false">Add Peripheral</button> </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div>
                <h2>Peripherals</h2>
                <table id="tableright">
                    <thead>
                        <tr>
                            <th>Count</th>
                            <th>Vendor</th>
                            <th>Date Created</th>
                            <th>Status</th>
                            <th>Buttons</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item,index) in peripherals.peripherals" :key="index">
                            <td>{{index+1}}</td>
                            <td>
                                {{ item.vendor }}
                            </td>
                            <td>{{item.dateCreated}}</td>
                            <td v-if="item.status==1">Online</td>
                            <td v-if="item.status==0">Offline</td>
                            <td><button @click="deletePeripheral(item.uid)">Delete Peripheral</button></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div id="form2" v-if="form2">
            <h4>Add Gateway</h4>

            <label for="SerialNumber">Serial Number:</label>
            <input v-model="serialNumber" name="SerialNumber" type="text" placeholder="Serial123" />
            <br />

            <label for="Hrname">HR name:</label>
            <input v-model="hrname" name="Hrname" type="text" placeholder="Something123" />
            <br />
            <label for="ipv4">IPv4</label>
            <input v-model="ipv4" name="ipv4" type="text" placeholder="1.1.1.1" />

            <button type="button" @click="addGateway()">Add Gateway</button>
        </div>

        <div v-if="form" id="form">

            <h5>Add Peripheral for selected gateway: {{selectedSN}}</h5>

            <label for="vendor">Vendor:</label>
            <input v-model="vendor" name="vendor" type="text" placeholder="Vendor1" />
            <br />
            <label for="status">Status:</label>
            <select v-model="status" name="status">
                <option>Online</option>
                <option>Offline</option>
            </select>

            <button type="button" @click="addPeripheral()">Add Peripheral</button>
        </div>
    </div>
</template>

<script>
    import 'bootstrap/dist/css/bootstrap.css';

    import axios from 'axios';
    export default {
        name: 'Home',
        
        data() {
            return {
                gateways: [],
                peripherals: "",
                form: false,
                form2:false,
                selectedSN: "",
                vendor: "",
                dateCreated: new Date(),
                status: "",
                serialNumber: "",
                hrname: "",
                ipv4: "",
            }
        },
         mounted() {
             axios
                 .get('http://localhost:1337/api/gateway')
                 .then((response) => {
                     this.gateways = response.data
                 },
                );

        },
         methods: {
             getPeripherals(sn) {
                 axios.get('http://localhost:1337/api/gateway/'+sn)
                     .then((response) => {
                         this.peripherals = response.data
                         console.log(this.peripherals)
                    },
                 );
             },
             addPeripheral() {
                 var statusp = 0;
                 if (this.status === "Online") 
                     statusp = 1
                 this.dateCreated = new Date();
                 axios.post('http://localhost:1337/api/peripheral', { "vendor": this.vendor, "dateCreated": this.dateCreated, "status": statusp, "gatewaySerialNumber": this.selectedSN }, ).then((res) => {
                     console.log(res.data)
                     location.reload()
                 }).catch((err) => {
                     alert("Too many peripherals, only 10 allowed")
                     console.log(err)
                 })
                 
                 this.form = false;
                
             },
             deletePeripheral(id) {
                 axios.delete('http://localhost:1337/api/peripheral/' + id).then((res) => {
                     console.log(res.data);
                     location.reload();
                 })
             },
             addGateway() {
                 if (this.serialNumber === "" || this.hrname == "" || this.ipv4 == "") {
                     alert("You must enter data in all fields")
                     return false;
                     

                 }
                 if (!(/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(this.ipv4))) { 
                     alert("You have entered an invalid IP address!")
                     return false;
                 }
                
                 axios.post('http://localhost:1337/api/gateway', { "serialNumber": this.serialNumber, "hrname": this.hrname, "ipv4": this.ipv4, "peripherals": [] },).then((res) => {
                     console.log(res.data)
                     location.reload()
                 }).catch((err) => {
                     alert(err)
                     console.log(err)
                 })
                 this.serialNumber = "";
                 this.hrname = "";
                 this.ipv4 = "";
                 this.form2 = false;
             }
        },
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    #form,#form2 {
        width: 500px;
        padding: 20px;
    }
    label{
        margin-right:1rem;
    }
    #form input, #form button, #form > select, #form2 input, #form2 button{
        margin:5px;
        padding:2px;
        width:300px;
        margin-left:0;
        padding-left:4px;
    }
    .datetimepicker{
        position:relative;
    }
    .wrap {
        display:grid;
        padding:20px;
        grid-template-columns:50% 50%;
        grid-column-gap:30px;
    }
    #table {
        font-family: Arial, Helvetica, sans-serif;
        border-collapse: collapse;
       display:block;
       margin-left:auto;
       margin-right:auto;
    }
                #table td, #table th {
                    border: 1px solid #ddd;
                    padding: 8px;
                }

                #table tr:nth-child(even) {
                    background-color: #f2f2f2;
                }

                #table tr:hover {
                    background-color: #ddd;
                }

                #table th {
                    padding-top: 12px;
                    padding-bottom: 12px;
                    text-align: left;
                    background-color: #04AA6D;
                    color: white;
                }

    #tableright {
        font-family: Arial, Helvetica, sans-serif;
        border-collapse: collapse;
        display: block;
        margin-left: auto;
        margin-right: auto;
    }

        #tableright td, #tableright th {
            border: 1px solid #ddd;
            padding: 8px;
        }

        #tableright tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        #tableright tr:hover {
            background-color: #ddd;
        }

        #tableright th {
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: left;
            background-color: #04AA6D;
            color: white;
        }
</style>

