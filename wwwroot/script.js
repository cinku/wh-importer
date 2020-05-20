document.getElementById('button').addEventListener('click', () => {
    const textareaValue = document.getElementById('textarea').value;

    const requestOptions = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(textareaValue)
    };

    fetch('/api/warehouses', requestOptions)
        .then(res => res.json())
        .then(warehouseData => printWarehouseData(warehouseData));
});

printWarehouseData = (warehouseData) => {
    const outputContainer = document.getElementById('output');
    outputContainer.innerHTML = '';

    warehouseData.forEach(({ Key: warehouseName, Value: warehouseMaterials }) => {
        const { materialsAmount, materials } = warehouseMaterials;

        outputContainer.innerHTML += `${warehouseName} (total: ${materialsAmount}) <br/>`;

        // we sort the materials as Object.entries doesnt guarantee to preserve order in any way
        const sortedMaterials = sortMaterialsInMagazine(Object.entries(materials));

        sortedMaterials.forEach(([key, value]) => outputContainer.innerHTML += `${key}: ${value} <br/>`);
        
        outputContainer.innerHTML += '<br />';
    });
};

sortMaterialsInMagazine = (materials) => {
    return materials.sort(([firstMaterialName], [secondMaterialName]) => {
        if (firstMaterialName < secondMaterialName) return -1;
        if (firstMaterialName > secondMaterialName) return 1;
        return 0;
    });
}