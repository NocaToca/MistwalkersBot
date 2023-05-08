
using Items;

namespace Characters{

    public partial class Character{

        /*************************************************************INVENTORY************************************************************************************************/
        public List<Item> items;

        int capacity;

        private void InitInventory(InventoryFields inventory_fields){
            this.capacity = inventory_fields.carrying_capacity;
            this.items = inventory_fields.items;
        }

/**************************************************************************************************************************************************************************/
    }

}